package yeah.hack.filizanka.controller.dto;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;
import yeah.hack.filizanka.model.Carriage;
import yeah.hack.filizanka.model.Egg;
import yeah.hack.filizanka.model.Skin;
import yeah.hack.filizanka.model.User;

import java.util.Set;
import java.util.stream.Collectors;

@Data
@AllArgsConstructor
@NoArgsConstructor
@Builder
public class UserDto {

    private Long userId;

    private String name;

    private Long activeSkinId;

    private Long credits;

    private Set<SkinDto> skins;

    private Set<CarriageDto> carriages;

    private Set<Long> eggIds;

    private PointDto destination;

    private PointDto lastVisited;

    private String currentTrainRide;

    public static UserDto from(User source) {

        return UserDto.builder()
                .userId(source.getUserId())
                .name(source.getName())
                .activeSkinId(source.getActiveSkin() == null ? 0 : source.getActiveSkin().getSkinId())
                .credits(source.getCredits())
                .skins(source.getSkins().stream().map(SkinDto::from).collect(Collectors.toSet()))
                .carriages(source.getCarriages().stream().map(CarriageDto::from).collect(Collectors.toSet()))
                .eggIds(source.getEggs().stream().map(Egg::getEggId).collect(Collectors.toSet()))
                .destination(source.getDestination() == null ? null : PointDto.from(source.getDestination()))
                .lastVisited(source.getLastPoint() == null ? null : PointDto.from(source.getLastPoint()))
                .currentTrainRide(source.getCurrentTrainRide() == null ? null : source.getCurrentTrainRide().getTrainRideId())
                .build();

    }

}
